"use client";
import { AggregatedPollDataByEndDateDto } from "@/types";
import { useState, useEffect } from "react";
import Loading from "./Loading";
import Plot from "react-plotly.js";

const AggregatedPollDataByEndDateChart = () => {
    const [data, setData] = useState<AggregatedPollDataByEndDateDto[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchPollData = async () => {
            try {
                setError(null);
                const apiUrl = `${process.env.NEXT_PUBLIC_API_URL}/GetAggregatedPollDataByEndDate`;

                const response = await fetch(apiUrl);

                if (response.ok) {
                    const responseData: AggregatedPollDataByEndDateDto[] = await response.json();

                    setData(responseData);
                    setLoading(false);
                } else {
                    console.error("Failed to fetch aggregated poll data by end week.");
                    setError("Failed to fetch aggregated poll data.");
                    setLoading(false);
                }
            } catch (error) {
                console.error("Failed to fetch aggregated poll data: ", error);
                setError("Failed to fetch aggregated poll data.");
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

    const dates = data.map((item) => item.endDate);
    const trumpAverages = data.map((item) => item.trumpAverage);
    const harrisAverages = data.map((item) => item.harrisAverage);

    // Calculate the date 30 days ago
    const endDate = new Date(); // Set end date to today
    const startDate = new Date(endDate);
    startDate.setDate(endDate.getDate() - 30);

    // Define the annotation date
    const bidenDroppedOutDate = "2024-07-21";

    return (
        <div className="w-full flex flex-col justify-center items-center mt-4">
           <h2 className="text-2xl font-bold text-center">Aggregated Poll Results by End Date</h2> 
                <Plot
                    data={[
                        {
                            x: dates,
                            y: trumpAverages,
                            type: "scatter",
                            mode: "lines+markers",
                            name: "Trump",
                            line: { color: "red" },
                        },
                        {
                            x: dates,
                            y: harrisAverages,
                            type: "scatter",
                            mode: "lines+markers",
                            name: "Harris",
                            line: { color: "blue" },
                        },
                    ]}
                    layout={{
                        xaxis: {
                            range: [startDate.toISOString().split('T')[0], endDate.toISOString().split('T')[0]],
                            rangeslider: { visible: true },
                        },
                        shapes: [
                            {
                                type: "line",
                                x0: bidenDroppedOutDate,
                                x1: bidenDroppedOutDate,
                                y0: 0,
                                y1: 1,
                                xref: "x",
                                yref: "paper",
                                line: {
                                    color: "green",
                                    width: 2,
                                    dash: "dashdot",
                                },
                            },
                        ],
                        annotations: [
                            {
                                x: bidenDroppedOutDate,
                                y: 1,
                                xref: "x",
                                yref: "paper",
                                text: "Biden dropped out",
                                showarrow: true,
                                arrowhead: 7,
                                ax: 0,
                                ay: -40,
                            },
                        ],
                        margin: {
                            t: 50
                        },
                    }}
                    useResizeHandler={true}
                    style={{ width: "100%", height:"100%" }}
                />
        </div>
    );
};

export default AggregatedPollDataByEndDateChart;