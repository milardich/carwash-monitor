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
        console.error(error);
        throw(error);
    }
}