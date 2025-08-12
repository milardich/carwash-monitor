import axios from 'axios'
import { type Box as Box } from '@/api/box.api'

export interface Station {
  id: string
  name: string
  boxes: Box[]
}

export interface CreateStationRequest {
  name: string
}

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL
})

export async function getAllStations(): Promise<Station[]> {
  const response = await axiosClient.get('/station')
  return response.data
}

export async function getStation(stationId: string): Promise<Station> {
  const response = await axiosClient.get(`/station/${stationId}`)
  return response.data
}

export async function createStation(request: CreateStationRequest): Promise<Station> {
  const response = await axiosClient.post(
    `/station`,
    {
      name: request.name
    },
    {
      headers: {
        'Content-Type': 'application/json'
      }
    }
  )
  return response.data
}
