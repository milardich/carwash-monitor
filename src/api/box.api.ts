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
  washCycles: WashCycle[]
}

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL
})

export async function getBox(boxId?: string): Promise<Box> {
  return await axiosClient.get(`/box/${boxId}`)
}

export async function changeBoxStatus(boxId: string, boxStatus: string): Promise<Box> {
  return await axiosClient.patch(`/box/${boxId}/status`, {
    status: boxStatus
  })
}

export async function createBox(stationId?: number): Promise<Station> {
  return await axiosClient.post(`/station/${stationId}/box`)
}
