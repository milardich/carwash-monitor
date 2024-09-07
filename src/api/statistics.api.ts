import axios from "axios";
import type { NumberLiteralType } from "typescript";

export interface StatisticsHighlights {
    totalRevenue: number;
    bestStationName: string;
    bestStationRevenue: number;
    totalWashCount: number;
    totalWaterConsumption: number;
    totalWaxConsumption: number;
    totalDetergentConsumption: number;
    totalWaterCost: number;
    totalWaxCost: number;
    totalDetergentCost: number;
}

export interface StationStatistics {
    stationName: string;
    waterCost: number;
    detergentCost: number;
    waxCost: number;
    revenue: number;
}

export interface StatisticsSummary {
    allStationStatistics: StationStatistics[];
    totalWaterCost: number;
    totalWaxCost: number;
    totalDetergentCost: number;
    totalRevenue: number;
}

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL,
});

// TODO: rename function to getStatisticsHighlights
export async function getStatistics(dateTimeFrom: string, dateTimeTo: string, timezone: string): Promise<StatisticsHighlights>{
    try {
        const { data } = await axiosClient.get(`/statistics/highlights`, {
            params: {
                dateTimeFrom: dateTimeFrom,
                dateTimeTo: dateTimeTo,
                timezone: timezone
            }
        });
        return data;
    } catch (error) {
        throw (error);
    }
}


export async function getStatisticsSummary(dateTimeFrom: string, dateTimeTo: string, timezone: string): Promise<StatisticsSummary>{
    try {
        const { data } = await axiosClient.get(`/statistics/summary`, {
            params: {
                dateTimeFrom: dateTimeFrom,
                dateTimeTo: dateTimeTo,
                timezone: timezone
            }
        });
        return data;
    } catch (error) {
        throw (error);
    }
}