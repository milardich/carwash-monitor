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


// SIMULATED WASHCYCLE
const simulatedStationId = ref<string>("")
const isSimulating = ref(false)

const handleSimulatedWashCycle = async () => {
    if (!simulatedStationId.value) {
        alert("Please select a station")
        return
    }

    const station = stationStore.stations.find(s => s.id === simulatedStationId.value)
    if (!station) return

    const availableBoxes = station.boxes.filter(b => b.status === "AVAILABLE")
    if (availableBoxes.length === 0) {
        alert("No available boxes in this station")
        return
    }

    const randomBox = availableBoxes[Math.floor(Math.random() * availableBoxes.length)]

    await boxStore.setBoxStatus(randomBox.id, "IN_USE")

    isSimulating.value = true

    setTimeout(async () => {
        const request = {
            coinAmount: Math.floor(Math.random() * 10) + 1,
            waterConsumption: Math.floor(Math.random() * 50) + 10,
            detergentConsumption: Math.floor(Math.random() * 20) + 5,
            waxConsumption: Math.floor(Math.random() * 10) + 2
        }

        await boxStore.setBoxStatus(randomBox.id, "AVAILABLE")
        await washCycleStore.createWashCycle(request, randomBox.id)

        isSimulating.value = false
    }, 10_000)
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
    <div class="p-4">
        <h1 class="text-3xl font-bold mb-6">Control Panel</h1>

        <div class="flex space-x-4">

            <!-- CREATE STATION -->
            <div class="p-6 border rounded-lg shadow-md w-80">
                <h2 class="text-xl font-semibold mb-4">Create Station</h2>
                <div class="flex flex-col space-y-3">
                    <input v-model="newStationName" type="text" placeholder="Station Name"
                        class="border rounded p-2 w-full" />
                    <button @click="handleCreateStation"
                        class="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 w-full">
                        Create Station
                    </button>
                </div>
            </div>

            <!-- CREATE BOX -->
            <div class="p-6 border rounded-lg shadow-md w-80">
                <h2 class="text-xl font-semibold mb-4">Create Box</h2>
                <div class="flex flex-col space-y-3">
                    <select v-model="selectedStationId" class="border rounded p-2 w-full">
                        <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                            {{ station.name }}
                        </option>
                    </select>
                    <button @click="handleCreateBox"
                        class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 w-full">
                        Create Box
                    </button>
                </div>
            </div>

            <!-- CREATE WASHCYCLE -->
            <div class="p-6 border rounded-lg shadow-md w-80">
                <h2 class="text-xl font-semibold mb-4">Create Wash Cycle</h2>
                <div class="flex flex-col space-y-3">
                    <!-- Station dropdown -->
                    <select v-model="washCycleSelectedStationId" @change="updateBoxesOnDropdownChange"
                        class="border rounded p-2 w-full">
                        <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                            {{ station.name }}
                        </option>
                    </select>

                    <!-- Box dropdown -->
                    <select v-model="selectedBoxId" class="border rounded p-2 w-full">
                        <option v-for="box in stationBoxes" :key="box.id" :value="box.id">
                            Box #{{ box.number }}
                        </option>
                    </select>

                    <!-- Input fields -->
                    <input v-model.number="coinAmount" type="number" placeholder="Coin Amount"
                        class="border rounded p-2 w-full" />
                    <input v-model.number="waterConsumption" type="number" placeholder="Water Consumption"
                        class="border rounded p-2 w-full" />
                    <input v-model.number="detergentConsumption" type="number" placeholder="Detergent Consumption"
                        class="border rounded p-2 w-full" />
                    <input v-model.number="waxConsumption" type="number" placeholder="Wax Consumption"
                        class="border rounded p-2 w-full" />

                    <button @click="handleCreateWashCycle"
                        class="bg-purple-500 text-white px-4 py-2 rounded hover:bg-purple-600 w-full">
                        Create Wash Cycle
                    </button>
                </div>
            </div>


            <!-- SIMULATED WASHCYCLE -->
            <div class="p-6 border rounded-lg shadow-md w-80">
                <h2 class="text-xl font-semibold mb-4">Simulated Wash Cycle</h2>
                <div class="flex flex-col space-y-3">
                    <!-- Station dropdown -->
                    <select v-model="simulatedStationId" class="border rounded p-2 w-full">
                        <option v-for="station in stationStore.stations" :key="station.id" :value="station.id">
                            {{ station.name }}
                        </option>
                    </select>

                    <button @click="handleSimulatedWashCycle"
                        class="bg-orange-500 text-white px-4 py-2 rounded hover:bg-orange-600 w-full"
                        :disabled="isSimulating">
                        {{ isSimulating ? 'Simulating...' : 'Start Simulation' }}
                    </button>
                </div>
            </div>

        </div>
    </div>
</template>
