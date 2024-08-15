import axios from 'axios';


export interface Unit {
    unitId: number; 
    status: string;
}

export interface UnitInfo {
    washCycleCount: number;
    totalCoinAmount: number;
    totalWaterConsumption: number;
    totalDetergentConsumption: number;
    totalWaxConsumption: number;
}

enum UnitStatus {
    IN_USE,
    INACTIVE,
    AVAILABLE
}

const axiosClient = axios.create({
    baseURL: "http://localhost:8080/api",
});

export async function getUnit(stationId: number, unitId: number): Promise<Unit> {
    try {
        const { data } = await axiosClient.get(`/station/${stationId}/unit/${unitId}`);
        return data;
    } catch (error) {
        throw(error);
    }
}

export async function getUnitInfo(dateTimeFrom: string, dateTimeTo: string, stationId?: number, unitId?: number): Promise<UnitInfo>{
    try {
        const { data } = await axiosClient.get(`/station/${stationId}/unit/${unitId}/info`, {
            params: {
                dateTimeFrom: dateTimeFrom,
                dateTimeTo: dateTimeTo
            }
        });
        return data;
    } catch (error) {
        throw(error);
    }
}

export async function changeUnitStatus(stationId: number, unitId: number, unitStatus: string): Promise<Unit> {
    try {
        const { data } = await axiosClient.patch(`/station/${stationId}/unit/${unitId}/status`, {
            status: unitStatus
        });
        return data;
    } catch (error) {
        throw(error);
    }
}