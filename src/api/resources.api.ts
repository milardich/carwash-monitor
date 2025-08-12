import axios from 'axios'

export interface ResourceSeries {
  labels: string[]
  data: number[]
}

export interface ChartResponse {
  water: ResourceSeries
  wax: ResourceSeries
  detergent: ResourceSeries
}

const axiosClient = axios.create({
  baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL
})

export async function getChartData(stationId: string): Promise<ChartResponse> {
  const response = await axiosClient.get(`/station/${stationId}/resources`)
  return response.data
}
