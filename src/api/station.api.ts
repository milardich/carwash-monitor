import axios from 'axios';
import { type Box as Box } from '@/api/box.api'

export interface Station {
    id: string;
    name: string;
    boxInfos: Box[];
}

export interface CreateStationRequest {
    name: string;
}

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL,
});

export async function getAllStations(): Promise<Station[]> {
    try {
        const { data } = await axiosClient.get('/station');
        return data;
    } catch (error) {
        throw (error);
    }
}

export async function getStation(stationId: string): Promise<Station> {
    try {
        const { data } = await axiosClient.get(`/station/${stationId}`);
        return data;
    } catch (error) {
        throw(error);
    }
}

export async function createStation(request: CreateStationRequest): Promise<Station> {
    try {
        const { data } = await axiosClient.post(`/station`, 
            {
                name: request.name,
            }, 
            { 
                headers: {
                    'Content-Type': 'application/json'
                }
            }
        );
        return data;
    } catch (error) {
        throw(error);
    }
}