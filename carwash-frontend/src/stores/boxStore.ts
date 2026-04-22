import { emptyTray, type Box } from '@/api/box.api'
import { defineStore } from 'pinia'
import { changeBoxStatus, createBox, deleteBox } from '@/api/box.api'
import { useStationStore } from './stationStore'

export const useBoxStore = defineStore('box', {
    state: () => ({
        boxPopupOpen: false,
        selectedBox: undefined as Box | undefined,
        refreshIntervalId: null as number | null
    }),

    actions: {
        setSelectedBox(box: Box) {
            this.selectedBox = box
        },

        toggleBoxPopup() {
            this.boxPopupOpen = !this.boxPopupOpen
        },

        async setSelectedBoxStatus(boxStatus: string) {
            if (!this.selectedBox) return

            this.selectedBox.status = boxStatus
            await changeBoxStatus(this.selectedBox.id, boxStatus)
            const stationStore = useStationStore()
            await stationStore.updateSelectedStation()
        },

        async setBoxStatus(boxId: string, boxStatus: string) {
            await changeBoxStatus(boxId, boxStatus)
            const stationStore = useStationStore()
            await stationStore.updateSelectedStation()
        },

        async createBox(stationId: string) {
            if (!stationId) return
            this.boxPopupOpen = false
            await createBox(stationId)
            const stationStore = useStationStore()
            await stationStore.updateSelectedStation()
        },

        async emptyTray(boxId?: string) {
            if (!boxId) return
            const result = await emptyTray(boxId)
            if (this.selectedBox) {
                this.selectedBox.coinTrayAmount = result.coinTrayAmount ?? 0
            }
            const stationStore = useStationStore()
            await stationStore.updateSelectedStation()
        },

        async deleteBox(boxId: string) {
            if (!boxId) return
            await deleteBox(boxId)
            const stationStore = useStationStore()
            await stationStore.loadStations()
        }
    }
})
