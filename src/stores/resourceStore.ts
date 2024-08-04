import type { ResourceConsumption } from '@/api/resources.api';
import { reactive } from 'vue';
import { getChartData } from '@/api/resources.api';

export const resourceStore = reactive({
    resourceConsumptions: [] as ResourceConsumption[],
    pgTimeInterval: "" as String
});

export async function setChartDataByStationId(stationId: number) {
    resourceStore.resourceConsumptions = [];
    var newData: ResourceConsumption[] = await getChartData(stationId, resourceStore.pgTimeInterval);
    resourceStore.resourceConsumptions = newData;
}