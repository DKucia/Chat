<template>
  <div class="message">
      <h5>{{message.username}}</h5>
      <p>{{message.content}}</p>
  </div>
</template>

<script>
export default {
    name:'Message',
    props:{
        message:Object
    }
}
</script>

<style scoped>
    .message{
        background-color: #CCF2CF;
        min-width: 50px;
        border-radius: 14px;
        margin-bottom: 10px;
        
    }
</style>