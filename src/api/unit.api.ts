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

export async function getUnitInfo(stationId: number, unitId: number, dateTimeFrom: string, dateTimeTo: string): Promise<UnitInfo>{
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