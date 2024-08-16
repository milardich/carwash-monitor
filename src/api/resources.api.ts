import axios from "axios";

export interface ResourceConsumption {
    washCycleDate: String,
    totalWaterConsumption: number,
    totalWaxConsumption: number,
    totalDetergentConsumption: number
}

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL,
});

export async function getChartData(stationId: number, pgTimeInterval: String): Promise<ResourceConsumption[]> {
    try {
        const { data } = await axiosClient.get(`/resources/station/${stationId}/resource-consumption/chart-data`, {
            params: {
                pgTimeInterval: pgTimeInterval
            }
        });
        return data;
    } catch (error) {
        throw (error);
    }
}