import axios from "axios";

export interface Statistics {
    totalRevenue: number;
    bestStationName: string;
    bestStationRevenue: number;
    totalWashCount: number;
    totalWaterConsumption: number;
    totalWaxConsumption: number;
    totalDetergentConsumption: number;
    totalWaterCost: number,
    totalWaxCost: number,
    totalDetergentCost: number
}


const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL,
});

export async function getStatistics(dateTimeFrom: string, dateTimeTo: string, timezone: string): Promise<Statistics>{
    try {
        const { data } = await axiosClient.get(`/statistics`, {
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