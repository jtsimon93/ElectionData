export interface PollDto {
    id: number;
    pollLink: string;
    pollster: string;
    startDate: string | null;
    endDate: string | null;
    sampleSize: string | null;
    sampleType: string | null;
    moE: number | null;
    trump: number | null;
    harris: number | null;
    spread: string | null;
}

export interface CandidateAveragesDto {
    trumpAverage: number;
    harrisAverage: number;
}

export interface AggregatedPollDataByEndDateDto {
    endDate: string;
    trumpAverage: number;
    harrisAverage: number;
}