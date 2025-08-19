<script setup lang="ts">
import { useBoxStore } from '@/stores/boxStore';
import { useStationStore } from '@/stores/stationStore';
import { useWashCycleStore } from '@/stores/washCycleStore';
import { ref, onMounted } from 'vue';
import { type Box } from '@/api/box.api';
import type { WashCycleRequest } from '@/api/washCycle.api';

const stationStore = useStationStore()
const boxStore = useBoxStore()
const washCycleStore = useWashCycleStore()


// STATION
const newStationName = ref('')
const handleCreateStation = async () => {
    if (!newStationName.value.trim()) {
        alert('Please enter a station name!')
        return
    }

    await stationStore.createStation(newStationName.value.trim())
    alert('Station created successfully!')
    await stationStore.loadStations()
    newStationName.value = ''
}


// BOX
const selectedStationId = ref('')
const handleCreateBox = async () => {
    if (!selectedStationId.value) {
        alert('Please select a station!')
        return
    }

    await boxStore.createBox(selectedStationId.value)
    alert('Box created successfully!')
}


// WASHCYCLE
const washCycleSelectedStationId = ref('')
const selectedBoxId = ref('')
const stationBoxes = ref<Box[]>([])
const coinAmount = ref(0)
const waterConsumption = ref(0)
const detergentConsumption = ref(0)
const waxConsumption = ref(0)

const updateBoxesOnDropdownChange = () => {
    const station = stationStore.stations.find(s => s.id === washCycleSelectedStationId.value)
    stationBoxes.value = station ? station.boxes : []
    selectedBoxId.value = stationBoxes.value.length ? stationBoxes.value[0].id : ''
}

const handleCreateWashCycle = async () => {
    if (!selectedBoxId.value) {
        alert('Please select a box!')
        return
    }

    const request: WashCycleRequest = {
        coinAmount: coinAmount.value,
        waterConsumption: waterConsumption.value,
        detergentConsumption: detergentConsumption.value,
        waxConsumption: waxConsumption.value
    }

    await washCycleStore.createWashCycle(request, selectedBoxId.value)
    alert('Wash Cycle created successfully!')

    coinAmount.value = 0
    waterConsumption.value = 0
    detergentConsumption.value = 0
    waxConsumption.value = 0
}

onMounted(async () => {
    await stationStore.loadStations()
    if (stationStore.stations.length) {
        selectedStationId.value = stationStore.stations[0].id
        washCycleSelectedStationId.value = stationStore.stations[0].id
        updateBoxesOnDropdownChange()
    }
})



</script>

<template>
    <div>
        <div class="p-4">
            <h1 class="text-3xl">Control Panel</h1>
        </div>

        <br>

        <div class="flex space-x-4">

            <!-- CREATE STATION -->
            <div class="p-4 border rounded shadow-md">
                <h2 class="text-xl font-semibold mb-2">Create Station</h2>
                <input v-model="newStationName" type="text" placeholder="Station Name"
                    class="border rounded p-2 mr-2" />
                <button @click="handleCreateStation"
                    class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600">
                    Create Station
                </button>
            </div>


            <!-- CREATE BOX -->
            <div class="p-4 border rounded shadow-md">
                <h2 class="text-xl font-semibold mb-2">Create Box</h2>

                <!-- Dropdown for selecting station -->
                <select v-model="selectedStationId" class="border rounded p-2 mr-2">
                    <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                        {{ station.name }}
                    </option>
                </select>

                <button @click="handleCreateBox" class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
                    Create Box
                </button>
            </div>


            <!-- CREATE WASHCYCLE -->
            <div class="p-4 border rounded shadow-md">
                <h2 class="text-xl font-semibold mb-2">Create Wash Cycle</h2>

                <!-- Station dropdown -->
                <select v-model="washCycleSelectedStationId" @change="updateBoxesOnDropdownChange"
                    class="border rounded p-2 mr-2 mb-2">
                    <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                        {{ station.name }}
                    </option>
                </select>

                <!-- Box dropdown -->
                <select v-model="selectedBoxId" class="border rounded p-2 mr-2 mb-2">
                    <option v-for="box in stationBoxes" :key="box.id" :value="box.id">
                        Box #{{ box.number }}
                    </option>
                </select>

                <!-- Input fields -->
                <input v-model.number="coinAmount" type="number" placeholder="Coin Amount"
                    class="border rounded p-2 mr-2 mb-2" />
                <input v-model.number="waterConsumption" type="number" placeholder="Water Consumption"
                    class="border rounded p-2 mr-2 mb-2" />
                <input v-model.number="detergentConsumption" type="number" placeholder="Detergent Consumption"
                    class="border rounded p-2 mr-2 mb-2" />
                <input v-model.number="waxConsumption" type="number" placeholder="Wax Consumption"
                    class="border rounded p-2 mr-2 mb-2" />

                <button @click="handleCreateWashCycle"
                    class="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600">
                    Create Wash Cycle
                </button>
            </div>
        </div>
    </div>
</template>
