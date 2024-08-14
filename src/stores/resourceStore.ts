import type { ResourceConsumption } from '@/api/resources.api';
import { reactive } from 'vue';
import { getChartData } from '@/api/resources.api';
import { useStationStore } from './stationStore';
import { computed } from 'vue';
import { defineStore } from 'pinia';

export const useResourceStore = defineStore('resources', {
    state: () => ({
        resourceConsumptions: [] as ResourceConsumption[],
        pgTimeInterval: "" as String,
    }),
    actions: {
        async setChartDataByStationId(stationId: number) {
            var newData: ResourceConsumption[] = await getChartData(stationId, this.pgTimeInterval);
            this.resourceConsumptions = newData;
        }
    },
});




// export const resourceStore = reactive({
//     resourceConsumptions: [] as ResourceConsumption[],
//     pgTimeInterval: "" as String,
// });

// export async function setChartDataByStationId(stationId: number) {
//     var newData: ResourceConsumption[] = await getChartData(stationId, resourceStore.pgTimeInterval);
//     if(newData[newData.length - 1] != resourceStore.resourceConsumptions[resourceStore.resourceConsumptions.length - 1]) {
//         resourceStore.resourceConsumptions = newData;
//     }
// }