<template>
  <div class="container mt-4">
    <div class="row">
      <!-- Cột Step -->
      <div class="col-md-4">
        <div class="step-container">
          <el-steps
            :active="active"
            direction="vertical"
            finish-status="success"
           :space="80"
          >
            <el-step title="Bước 1" description="Chọn item từ bảng A → B" />
            <el-step title="Bước 2" description="Lấy issue trong Sprint" />
            <el-step title="Bước 3" description="Xác nhận dữ liệu" />
            <el-step title="Bước 4" description="Hoàn tất" />
            <el-step title="Bước 5" description="Tùy chọn thêm" />
            <!-- Add more steps here if needed -->
          </el-steps>
        </div>
      </div>

      <!-- Cột nội dung bảng -->
      <div class="col-md-8">
        <div v-if="active === 0" class="d-flex justify-content-between">
          <!-- Bảng A -->
          <div class="w-45 border rounded p-2 bg-light">
            <h5 class="text-center">Bảng A</h5>
            <el-table
              :data="tableAData"
              @selection-change="handleASelectionChange"
              size="small"
              height="250"
            >
              <el-table-column type="selection" width="40" />
              <el-table-column prop="name" label="Tên" />
            </el-table>
          </div>

          <!-- Nút chuyển -->
          <div class="d-flex flex-column justify-content-center align-items-center mx-2">
            <el-button @click="moveToB" size="small" type="primary" plain>&gt;&gt;</el-button>
            <el-button @click="moveToA" size="small" type="primary" plain>&lt;&lt;</el-button>
          </div>

          <!-- Bảng B -->
          <div class="w-45 border rounded p-2 bg-light">
            <h5 class="text-center">Bảng B</h5>
            <el-table
              :data="tableBData"
              @selection-change="handleBSelectionChange"
              size="small"
              height="250"
            >
              <el-table-column type="selection" width="40" />
              <el-table-column prop="name" label="Tên" />
            </el-table>
          </div>
        </div>

        <!-- Các Step khác -->
        <div v-if="active === 1">
          <p class="alert alert-info">Bạn đang ở bước 2.</p>
        </div>
        <div v-if="active === 2">
          <p class="alert alert-success">Cấu hình hoàn tất!</p>
        </div>

        <!-- Nút Next -->
        <div class="mt-3">
          <el-button type="success" @click="next">Next step</el-button>
          <el-button type="primary" @click="prev">Previous step</el-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const active = ref(0)
const next = () => {
  if (active.value < 5) {
    active.value++
  } else {
    active.value = 0
  }
}

const prev = () => {
  if (active.value > 0) {
    active.value--
  } else {
    active.value = 5
  }
}

// Dữ liệu bảng
const tableAData = ref([
  { id: 1, name: 'Item A1' },
  { id: 2, name: 'Item A2' },
  { id: 3, name: 'Item A3' }
])
const tableBData = ref([])

const selectedA = ref([])
const selectedB = ref([])

const handleASelectionChange = (val) => {
  selectedA.value = val
}
const handleBSelectionChange = (val) => {
  selectedB.value = val
}

const moveToB = () => {
  const selectedIds = selectedA.value.map(item => item.id)
  const movedItems = tableAData.value.filter(item => selectedIds.includes(item.id))
  tableBData.value.push(...movedItems)
  tableAData.value = tableAData.value.filter(item => !selectedIds.includes(item.id))
  selectedA.value = []
}

const moveToA = () => {
  const selectedIds = selectedB.value.map(item => item.id)
  const movedItems = tableBData.value.filter(item => selectedIds.includes(item.id))
  tableAData.value.push(...movedItems)
  tableBData.value = tableBData.value.filter(item => !selectedIds.includes(item.id))
  selectedB.value = []
}
</script>

<style scoped>
.w-45 {
  width: 45%;
}
.step-container {
  max-height: 500px; /* hoặc auto nếu không giới hạn */
  overflow-y: auto;
  padding-right: 10px;
  border: 1px solid #ccc;
}
</style>
