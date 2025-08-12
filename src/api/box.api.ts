import axios from 'axios'
import { type Station } from './station.api'
import type { WashCycle } from './washCycle.api'

export interface Box {
    id: string
    status: string
    number: number
    washCycleCount: number
    totalCoinAmount: number
    totalWaterConsumption: number
    totalDetergentConsumption: number
    totalWaxConsumption: number
    coinTrayAmount: number
    coinTrayLimit: number
    washCycles: WashCycle[]
}

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL
})

export async function getBox(boxId?: string): Promise<Box> {
    const response = await axiosClient.get(`/box/${boxId}`)
    return response.data
}

export async function changeBoxStatus(boxId: string, boxStatus: string): Promise<Box> {
    const response = await axiosClient.patch(`/box/${boxId}/status`, {
        status: boxStatus
    })
    return response.data
}

export async function createBox(stationId?: number): Promise<Station> {
    const response = await axiosClient.post(`/station/${stationId}/box`)
    return response.data
}
