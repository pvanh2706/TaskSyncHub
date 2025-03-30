<template>
    <el-table :data="tasks" border>
        <el-table-column prop="title" label="Công việc" />
        <el-table-column label="Hành động">
            <template #default="{ row }">
                <el-button type="danger" @click="$emit('deleteTask', row.id)">Xóa</el-button>
            </template>
        </el-table-column>
    </el-table>
</template>
  
<script>
export default {
    name: 'TaskList',
    props: {
        tasks: Array
    },
    // emits: ['deleteTask']
}
</script>
  
  