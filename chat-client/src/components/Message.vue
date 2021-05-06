<template>
  <div class="message">
      <img :src="message.userInfo.avatar"/>
      <h5>{{message.userInfo.username}}</h5>
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
        width: 50%;
        border-radius: 14px;
        margin-bottom: 10px;
        
    }

    img{
        border-radius: 50%;
        width: 40px;
        height: 40px;
    }
</style>