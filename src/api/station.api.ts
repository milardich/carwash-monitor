import axios from 'axios';
import { type Unit } from '@/api/unit.api'

export interface Station {
    stationId: number;
    stationName: string;
    city: string;
    streetName: string;
    streetNumber: string;
    country: string;
    units: Unit[];
}

export interface CreateStationRequest {
    stationName: string;
    city: string;
    streetName: string;
    streetNumber: string;
    country: string;
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

export async function getStation(stationId: number): Promise<Station | undefined> {
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
                stationName: request.stationName,
                city: request.city,
                streetName: request.streetName,
                streetNumber: request.streetNumber,
                country: request.country
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