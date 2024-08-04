import type { ResourceConsumption } from '@/api/resources.api';
import { reactive } from 'vue';

export const resourceStore = reactive({
    resourceConsumptions: [] as ResourceConsumption[],
    pgTimeInterval: "" as String
});
