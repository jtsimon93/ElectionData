"use client";
import React, { useEffect, useState } from 'react';
import { PollDto } from '@/types';
import Loading from './Loading';

type LatestPollInfoBoxProps = {
    initialPollData?: PollDto | null;
}

const LatestPollInfoBox = ({ initialPollData = null }: LatestPollInfoBoxProps) => {
    const [pollData, setPollData] = useState<PollDto | null>(initialPollData);
    const [error, setError] = useState<string | null>(null);
    const [leaderMessage, setLeaderMessage] = useState<string | null>(null);
    const [leaderMessageColor, setLeaderMessageColor] = useState<string | null>(null);

    useEffect(() => {
        const fetchPollData = async () => {
            try {
                const apiUrl = `${process.env.NEXT_PUBLIC_API_URL}/GetLatestPoll`;

                const response = await fetch(apiUrl);

                if (response.ok) {
                    const data: PollDto = await response.json();

                    // Parse to 2 decimal places
                    if (data.trump != null) {
                        data.trump = parseFloat(data.trump.toFixed(2));
                    }
                    if (data.harris != null) {
                        data.harris = parseFloat(data.harris.toFixed(2));
                    }

                    setPollData(data);

                    if (data.harris != null && data.trump != null) {
                        const winner = data.trump > data.harris ? "Trump" : "Harris";
                        const points = Math.abs(data.trump - data.harris).toFixed(2);
    
                        const message = `${winner} is leading by ${points} points!`
                        setLeaderMessage(message);

                        if(winner == 'Trump') {
                            setLeaderMessageColor("text-red-500");
                        }
                        else {
                            setLeaderMessageColor("text-blue-500");
                        }
                    }
                } else {
                    console.error("Failed to fetch poll data. Status:", response.status);
                    setError("Failed to fetch poll data.");
                }

            } catch (error) {
                console.error("Error fetching poll data:", error);
                setError("Error fetching poll data.");
            }
        };

        fetchPollData();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    if (pollData == null) {
        return <Loading />;
    }

    return (
        <div className="border border-gray-300 rounded-lg p-6 max-w-md mx-auto bg-white shadow-md">
            <h2 className="text-2xl font-bold text-center text-gray-800 mb-4">Latest Poll Results</h2>
            <div className="flex flex-col items-center">
                <p className="text-xl mb-2"><strong>Trump:</strong> {pollData.trump}%</p>
                <p className="text-xl mb-2"><strong>Harris:</strong> {pollData.harris}%</p>
                <p className={`mb-2 text-base font-bold ${leaderMessageColor}`}>{leaderMessage}</p>
                <p className="text-xs">Latest poll ended on: {pollData.endDate ? new Date(pollData.endDate).toLocaleDateString() : 'N/A'}</p>
            </div>
        </div>
    );
}

export default LatestPollInfoBox;