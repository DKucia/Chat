<template>
  <div class="conversations" >
        <ConversationItem  :conversation="conversation" v-for="conversation in conversations" :key="conversation.id"/>
      </div>
</template>

<script>
import ConversationItem from '../components/ConversationItem';
import {mapActions,mapGetters} from 'vuex';

export default {
    components:{
        ConversationItem
    },
    name:'ConversationsList',
    methods:{
        ...mapActions(['loadConversations','selectConversation']),
    },
    computed:{
        ...mapGetters(['conversations']),
    },
    created(){
        this.loadConversations();
    }
}
</script>

<style scoped>
    .conversations{
        cursor: pointer;
        box-sizing: border-box;
        padding: 15px;
        width: 25%;
        display: flex;
        height: 100%;
        border: 1px solid grey;
    }
</style>