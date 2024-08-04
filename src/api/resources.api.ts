import axios from "axios";

export interface ResourceConsumption {
    washCycleDate: String,
    totalWaterConsumption: number,
    totalWaxConsumption: number,
    totalDetergentConsumption: number
}

const axiosClient = axios.create({
    baseURL: "http://localhost:8080/api/resources/station/"
});

export async function getChartData(stationId: number, pgTimeInterval: String): Promise<ResourceConsumption[]> {
    try {
        const { data } = await axiosClient.get(stationId + '/resource-consumption/chart-data', {
            params: {
                pgTimeInterval: pgTimeInterval
            }
        });
        return data;
    } catch (error) {
        throw (error);
    }
}