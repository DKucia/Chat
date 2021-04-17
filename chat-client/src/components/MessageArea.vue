<template>
  <div class="message-area">
      <MessageList />
      <MessageInput /> 
  </div>
</template>

<script>
import MessageInput from './MessageInput';
import MessageList from './MessageList';


export default {
    name:'MessageArea',
    components:{
        MessageInput,
        MessageList
    },
}
</script>

<style scoped>
    .message-area{
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
        border:1px gold solid;
    }
</style>