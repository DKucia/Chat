<template>
<div v-show="message">
  <h5>{{message}}</h5>
</div>

</template>

<script>
export default {
    name:"ErrorMessage",
    props:{
        message:String,
    },
}
</script>

<style scoped>
    div{
        color:red;
    }
</style>