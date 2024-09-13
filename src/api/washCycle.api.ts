import axios from "axios";

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL,
});

export interface WashCycleRequest {
    waterConsumption: number;
    detergentConsumption: number;
    waxConsumption: number;
    coinAmount: number;
}

export interface WashCycle {
    washCycleId: number;
    stationId: number;
    unitId: number;
    washCycleDate: string,
    coinAmount: number;
    waterConsumption: number;
    detergentConsumption: number;
    waxConsumption: number;
}

export async function createWashCycle(request: WashCycleRequest, stationId?: number, unitId?: number): Promise<WashCycle> {
    try {
        const { data } = await axiosClient.post(`/station/${stationId}/unit/${unitId}/wash-cycle`, 
            {
                waterConsumption: request.waterConsumption,
                detergentConsumption: request.detergentConsumption,
                waxConsumption: request.waxConsumption,
                coinAmount: request.coinAmount
            },
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );
        return data;
    } catch (error) {
        throw (error);
    }
}