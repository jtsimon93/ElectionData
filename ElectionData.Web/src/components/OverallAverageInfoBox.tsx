"use client";
import React, { useEffect, useState } from 'react';
import { CandidateAveragesDto } from '@/types';
import Loading from './Loading';

const OverallAverageInfoBox = () => {
    const [averageData, setAverageData] = useState<CandidateAveragesDto | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [leaderMessage, setLeaderMessage] = useState<string | null>(null);
    const [leaderMessageColor, setLeaderMessageColor] = useState<string | null>(null);

    useEffect(() => {
        const fetchAverageData = async () => {
            try {
                const apiUrl = `${process.env.NEXT_PUBLIC_API_URL}/GetCandidateAverages`;

                const response = await fetch(apiUrl);

                if (response.ok) {
                    const data: CandidateAveragesDto = await response.json();

                    // Round the averages to two decimal places
                    data.trumpAverage = Number(data.trumpAverage.toFixed(2));
                    data.harrisAverage = Number(data.harrisAverage.toFixed(2));


                    setAverageData(data);

                    if (data.trumpAverage != null && data.harrisAverage != null) {
                        const winner = data.trumpAverage > data.harrisAverage ? "Trump" : "Harris";
                        const points = Math.abs(data.trumpAverage - data.harrisAverage).toFixed(2);
    
                        const message = `${winner} is leading by ${points}!`
                        setLeaderMessage(message);

                        if(winner == 'Trump') {
                            setLeaderMessageColor("text-red-500");
                        }
                        else {
                            setLeaderMessageColor("text-blue-500");
                        }
                    }
                } else {
                    console.error("Failed to fetch average data. Status:", response.status);
                    setError("Failed to fetch average data.");
                }

            } catch (error) {
                console.error("Error fetching average data:", error);
                setError("Error fetching average data.");
            }
        };

        fetchAverageData();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    if (averageData == null) {
        return <Loading />;
    }

    return (
        <div className="border border-gray-300 rounded-lg p-6 max-w-md mx-auto bg-white shadow-md">
            <h2 className="text-2xl font-bold text-center text-gray-800 mb-4">Overall Average Results</h2>
            <div className="flex flex-col items-center">
                <p className="text-xl mb-2"><strong>Trump Average:</strong> {averageData.trumpAverage}%</p>
                <p className="text-xl mb-2"><strong>Harris Average:</strong> {averageData.harrisAverage}%</p>
                <p className={`mb-2 text-base font-bold ${leaderMessageColor}`}>{leaderMessage}</p>
            </div>
        </div>
    );
}

export default OverallAverageInfoBox;