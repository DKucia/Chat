<template>
  <div class="conversation-item " :class="{active: isActive }" @click="select">
      <img src="../assets/avatar.png" alt="f"/>
      <div>
          <h4>{{conversation.name}}</h4>
          <p>text form conversation</p>
      </div>
  </div>
</template>

<script>
import {mapActions,mapGetters} from 'vuex';
export default {
    name:"ConversationItem",
    computed:{
        ...mapGetters(["selectedConverationId"]),
        isActive(){
            return this.conversation.id===this.selectedConverationId;
        }
    },
    props:{
        conversation:Object,
    },
    methods:{
        ...mapActions(["selectConversation"]),
        select(){
            this.selectConversation(this.conversation.id);
        }
    }
}
</script>

<style scoped>
    .conversation-item{
        cursor: pointer;
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
        display: block;
        width: 80px;
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