import axios from 'axios';
import { type Station } from './station.api';

export interface Box {
    id: string; 
    status: string;
    number: number;
}

export interface BoxInfo {
    washCycleCount: number;
    totalCoinAmount: number;
    totalWaterConsumption: number;
    totalDetergentConsumption: number;
    totalWaxConsumption: number;
    status: string;
}

enum BoxStatus {
    IN_USE,
    INACTIVE,
    AVAILABLE
}

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_CARWASH_API_BASE_URL,
});

export async function getBox(boxId?: number): Promise<Box> {
    try {
        const { data } = await axiosClient.get(`/box/${boxId}`);
        return data;
    } catch (error) {
        throw(error);
    }
}

export async function getBoxInfo(boxId?: string): Promise<BoxInfo>{
    try {
        const { data } = await axiosClient.get(`/box/${boxId}/info`);
        return data;
    } catch (error) {
        throw(error);
    }
}

export async function changeBoxStatus(boxId: string, boxStatus: string): Promise<Box> {
    try {
        const { data } = await axiosClient.patch(`/box/${boxId}/status`, {
            status: boxStatus
        });
        return data;
    } catch (error) {
        throw(error);
    }
}

export async function createBox(stationId?: number): Promise<Station> {
    try {
        const { data } = await axiosClient.post(`/station/${stationId}/box`);
        return data;
    } catch (error) {
        throw (error);
    }
}