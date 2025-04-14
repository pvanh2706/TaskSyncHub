<template>
    <el-card class="w-full max-w-6xl mx-auto mt-10">
      <template #header>
        <div class="text-xl font-bold">Tạo danh sách Jira Issue</div>
      </template>
  
      <el-table :data="issues" border style="width: 100%">
        <!-- Project Key -->
        <el-table-column prop="projectKey" label="Project" width="180">
          <template #default="{ row }">
            <el-select v-model="row.projectKey" placeholder="Chọn project" filterable>
              <el-option
                v-for="item in projectOptions"
                :key="item.key"
                :label="item.name"
                :value="item.key"
              />
            </el-select>
          </template>
        </el-table-column>
  
        <!-- Issue Type -->
        <el-table-column prop="issueTypeId" label="Issue Type" width="180">
          <template #default="{ row }">
            <el-select v-model="row.issueTypeId" placeholder="Chọn issue type" filterable>
              <el-option
                v-for="item in issueTypeOptions"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              />
            </el-select>
          </template>
        </el-table-column>
  
        <!-- Component -->
        <el-table-column prop="componentId" label="Component" width="180">
          <template #default="{ row }">
            <el-select v-model="row.componentId" placeholder="Chọn component" filterable>
              <el-option
                v-for="item in componentOptions"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              />
            </el-select>
          </template>
        </el-table-column>
  
        <!-- Summary -->
        <el-table-column prop="summary" label="Summary" width="200">
          <template #default="{ row }">
            <el-input v-model="row.summary" placeholder="Mô tả" />
          </template>
        </el-table-column>
  
        <!-- Assignee -->
        <el-table-column prop="assigneeName" label="Assignee" width="150">
          <template #default="{ row }">
            <el-input v-model="row.assigneeName" placeholder="Tên người nhận" />
          </template>
        </el-table-column>
  
        <!-- Custom Field -->
        <el-table-column prop="customField12815" label="CustomField 12815" width="180">
          <template #default="{ row }">
            <el-input v-model="row.customField12815" />
          </template>
        </el-table-column>
  
        <!-- Actions -->
        <el-table-column label="Hành động" width="100">
          <template #default="{ $index }">
            <el-button type="danger" icon="el-icon-delete" @click="removeRow($index)" />
          </template>
        </el-table-column>
      </el-table>
  
      <!-- Add & Submit -->
      <div class="flex justify-between mt-4">
        <el-button type="primary" @click="addRow">Thêm dòng</el-button>
        <el-button type="success" :loading="loading" @click="submitAll">Gửi tất cả</el-button>
        <el-button type="warning" @click="scheduleAutoSubmit">Hẹn giờ gửi lúc 18:00</el-button>
      </div>
  
      <el-alert
        v-if="result.message"
        :title="result.message"
        :type="result.success ? 'success' : 'error'"
        show-icon
        class="mt-4"
      />

      <el-alert
        v-if="scheduled"
        title="⏰ Đã hẹn giờ gửi tự động lúc 18:00."
        type="info"
        show-icon
        class="mt-2"
      />
    </el-card>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue'
  import axios from 'axios'

  const scheduled = ref(false)
  // let timerId = null
  // Dữ liệu bảng
  const issues = ref([
    {
      projectKey: '',
      summary: '',
      issueTypeId: '',
      assigneeName: '',
      parentKey: '',
      componentId: '',
      customField12815: '',
      customField13419: '',
      customField14332: '',
      customField14338: '',
      customField14018: '',
      customField12412: '',
      customField12413: '',
      customField13630: ''
    }
  ])
  
  // Options cho dropdown
  const projectOptions = ref([])
  const issueTypeOptions = ref([])
  const componentOptions = ref([])
  
  const loading = ref(false)
  const result = ref({ message: '', success: false })
  
  const addRow = () => {
    issues.value.push({
      projectKey: '',
      summary: '',
      issueTypeId: '',
      assigneeName: '',
      parentKey: '',
      componentId: '',
      customField12815: '',
      customField13419: '',
      customField14332: '',
      customField14338: '',
      customField14018: '',
      customField12412: '',
      customField12413: '',
      customField13630: ''
    })
  }
  
  const removeRow = (index) => {
    issues.value.splice(index, 1)
  }
  
  const submitAll = async () => {
    loading.value = true
    result.value = { message: '', success: false }
  
    try {
      const responses = await Promise.all(
        issues.value.map(issue => axios.post('/api/create-and-transition', issue))
      )
  
      result.value = {
        message: `Tạo thành công ${responses.length} issue.`,
        success: true
      }
    } catch (err) {
      result.value = {
        message: 'Lỗi khi tạo hoặc chuyển trạng thái issue.',
        success: false
      }
    } finally {
      loading.value = false
    }
  }
  const scheduleAutoSubmit = () => {
    const now = new Date()
    const sixPM = new Date()
    sixPM.setHours(18, 0, 0, 0) // 18:00:00.000

    // Nếu đã qua 18h hôm nay → hẹn giờ vào ngày mai
    if (now >= sixPM) {
        sixPM.setDate(sixPM.getDate() + 1)
    }

    // const delay = sixPM.getTime() - now.getTime()
    scheduled.value = true

    // timerId = setTimeout(async () => {
    //     await submitAll()
    //     scheduled.value = false
    // }, delay)
  }
  // 🟡 Fetch dữ liệu dropdown giả lập (hoặc gọi từ API thật)
  onMounted(() => {
    // Bạn có thể gọi từ API thực tế tại đây
    projectOptions.value = [
      { key: 'EAS', name: 'EZCloud System' },
      { key: 'DEV', name: 'Development' },
      { key: 'MKT', name: 'Marketing' }
    ]
  
    issueTypeOptions.value = [
      { id: '10100', name: 'Task' },
      { id: '10101', name: 'Bug' },
      { id: '10102', name: 'Story' }
    ]
  
    componentOptions.value = [
      { id: '12345', name: 'Booking Engine' },
      { id: '67890', name: 'Front Desk' },
      { id: '11111', name: 'POS' }
    ]
  })
  </script>
  
  <style scoped>
  .el-card {
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
  }
  </style>
  