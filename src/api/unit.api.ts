import axios from 'axios';


export interface Unit {
    unitId: number; 
    status: string;
    coinTrayAmount: number;
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
    baseURL: "http://localhost:8080/api/station/{stationId}",
});

export async function getUnit(stationId: number, unitId: number) {
    try {
        const { data } = await axiosClient.get('/unit/${unitId}');
        return [null, data];
    } catch (error) {
        return [error];
    }
}
