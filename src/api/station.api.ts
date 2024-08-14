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
    baseURL: "http://localhost:8080/api/station",
});

export async function getAllStations(): Promise<Station[]> {
    try {
        const { data } = await axiosClient.get('');
        return data;
    } catch (error) {
        throw (error);
    }
}

export async function getStation(stationId: number): Promise<Station | undefined> {
    try {
        const { data } = await axiosClient.get(`/${stationId}`);
        return data;
    } catch (error) {
        console.error(error);
        throw(error);
    }
}