import axios from 'axios'

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL
})

export interface WashCycleRequest {
    waterConsumption: number
    detergentConsumption: number
    waxConsumption: number
    coinAmount: number
}

export interface WashCycle {
    id: string
    dateCreated: string
    coinAmount: number
    waterConsumption: number
    detergentConsumption: number
    waxConsumption: number
}

export async function createWashCycle(
    request: WashCycleRequest,
    boxId?: number
): Promise<WashCycle> {
    const response = await axiosClient.post(
        `/box/${boxId}/washcycle`,
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
    )
    return response.data
}
