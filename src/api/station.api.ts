import axios from 'axios';

export interface Station {
    stationId: number;
    stationName: string;
    city: string;
    streetName: string;
    streetNumber: string;
    country: string;
}

const axiosClient = axios.create({
    baseURL: "http://localhost:8080/api/station",
});

export async function getAllStations() {
    try {
        const { data } = await axiosClient.get('');
        return [null, data];
    } catch (error) {
        return [error];
    }
}

export async function getStation(stationId: number): Promise<Station | null> {
    try {
        const { data } = await axiosClient.get(`/${stationId}`);
        return data;
    } catch (error) {
        console.error(error);
        return null;
    }
}