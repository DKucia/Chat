<template>
  <div class="conversation-item active">
      <img src="../assets/avatar.png" alt="f"/>
      <div>
          <h4>{{conversation.name}}</h4>
          <p>text form conversation</p>
      </div>
  </div>
</template>

<script>
export default {
    name:"ConversationItem",
    props:{
        conversation:Object,
    }
}
</script>

<style scoped>
    .conversation-item{
        box-sizing: border-box;
        padding:  10px;
        border-radius: 8px;
        display: flex;
        height: 80px;
        width: 100%;
        margin-bottom: 10px;
    }

    .active{
         background-color: #E5EFFA;
    }

    img{
        margin-right: 15px;
    }
    h4{
        text-align: left;
        margin: 0;
        margin-block-start: 0;
    }
    p{
        font-size: 15px;
        color:grey;
    }
</style>