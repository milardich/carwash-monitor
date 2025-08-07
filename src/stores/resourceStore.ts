import type { ResourceConsumption } from '@/api/resources.api';
import { getChartData } from '@/api/resources.api';
import { defineStore } from 'pinia';

export const useResourceStore = defineStore('resources', {
    state: () => ({
        resourceConsumptions: [] as ResourceConsumption[],
        pgTimeInterval: "" as String,
    }),
    actions: {
        async setChartDataByStationId(stationId: string) {
            var newData: ResourceConsumption[] = await getChartData(stationId, this.pgTimeInterval);
            this.resourceConsumptions = newData;
        }
    },
});