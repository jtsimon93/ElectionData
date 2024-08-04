"use client";
import { AggregatedPollDataByPollsterDto } from "@/types";
import { useEffect, useState } from "react";
import Loading from "./Loading";
import dynamic from "next/dynamic";

const AggregatedPollDataByPollsterChart = () => {
    const Plot = dynamic(() => import('react-plotly.js'), { ssr: false });
    const [data, setData] = useState<AggregatedPollDataByPollsterDto[] | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchPollData = async () => {
            try {
                setLoading(true);
                setError(null);

                const apiUrl = `${process.env.NEXT_PUBLIC_API_URL}/GetAggregatedPollDataByPollster`;

                const response = await fetch(apiUrl);

                if (response.ok) {
                    const responseData: AggregatedPollDataByPollsterDto[] = await response.json();

                    setData(responseData);
                    setLoading(false);
                    setError(null);
                } else {
                    setError("Failed to fetch aggregated poll data.");
                    setLoading(false);
                }
            } catch (error) {
                setError("Error fetching aggregated poll data.");
                setLoading(false);
            }
        };

        fetchPollData();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    if (data == null || loading) {
        return <Loading />;
    }

    // Prepare the data for the chart
    const pollsters = data.map(d => d.pollster);
    const trumpAverages = data.map(d => d.trumpAverage);
    const harrisAverages = data.map(d => d.harrisAverage);

    return (
        <div className="w-full flex flex-col justify-center items-center mt-4 border border-gray-300 rounded-lg py-6 px-4 bg-white shadow-md">
            <h2 className="text-2xl font-bold text-center">Aggregated Poll Results by Pollster</h2>
            <Plot
                data={[
                    {
                        x: pollsters,
                        y: trumpAverages,
                        type: 'bar',
                        name: 'Trump Average',
                        marker: { color: 'red' }
                    },
                    {
                        x: pollsters,
                        y: harrisAverages,
                        type: 'bar',
                        name: 'Harris Average',
                        marker: { color: 'blue' }
                    }
                ]}
                layout={{
                    barmode: 'group',
                    xaxis: { title: 'Pollster' },
                    yaxis: { title: 'Average', range: [Math.min(...trumpAverages, ...harrisAverages) - 5, Math.max(...trumpAverages, ...harrisAverages) + 5] },
                    margin: { t: 50 },
                }}
                useResizeHandler={true}
                style={{ width: "100%", height: "100%" }}
            />
        </div>
    );
};

export default AggregatedPollDataByPollsterChart;