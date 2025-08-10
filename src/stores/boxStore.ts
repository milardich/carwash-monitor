import { getBox, type Box } from '@/api/box.api'
import { defineStore } from 'pinia'

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

    async getBox(boxId: string) {
      this.selectedBox = await getBox(boxId)
    },

    toggleBoxPopup() {
      this.boxPopupOpen = !this.boxPopupOpen
    },

    setBoxStatus(boxState: string) {
      if (this.selectedBox) {
        this.selectedBox.status = boxState
      }
    },

    startAutoRefresh(boxId: string) {
      this.stopAutoRefresh()
      this.refreshIntervalId = window.setInterval(() => {
        this.getBox(boxId)
      }, 60_000)
    },

    stopAutoRefresh() {
      if (this.refreshIntervalId !== null) {
        clearInterval(this.refreshIntervalId)
        this.refreshIntervalId = null
      }
    }
  }
})
