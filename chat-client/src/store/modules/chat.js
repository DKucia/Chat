import axios from "axios";
import router from "../../router";

const UNAUTHORIZED = 401;
axios.interceptors.response.use(
  response => response,
  error => {
    const {status} = error.response;
    if (status === UNAUTHORIZED) {
        localStorage.removeItem('token');
        router.push('/login');
    }
    return Promise.reject(error);
 }
);

const state = {
  conversations: [],
  messages:[],
  selectedConverationId:null
};

const getters = {
  conversations: (state) => state.conversations,
  messages: state=> state.messages,
  selectedConverationId: state=>state.selectedConverationId
};

const actions = {
  async loadConversations({ commit,dispatch }) {
    let response = await axios.get(
      "https://localhost:44310/api/chat/conversations",{
        'headers':{
            'Authorization':`Bearer ${localStorage.getItem('token')}`
        }
      }
    );
    console.log(response.data);
    commit("setConversations", response.data);
    dispatch('selectConversation',response.data[0].id)
  },

  selectConversation({commit,dispatch},id){
    commit('setSeletedConversation',id);
    dispatch('loadMessages',id)
  },

  async loadMessages({commit},conversationId){
    let response = await axios.get(
        `https://localhost:44310/api/chat/messages/${conversationId}`,{
          'headers':{
              'Authorization':`Bearer ${localStorage.getItem('token')}`
          }
        }
      );
      commit('setMessages',response.data);
  },
  addMessage({commit},message){
    commit('newMessage',message);
  }
};

const mutations = {
  setConversations(state, data) {
    state.conversations = data;
  },

  setSeletedConversation(state,id){
      state.selectedConverationId=id;
  },
  setMessages(state,messages){
    state.messages=messages;
  },
  newMessage(state,message){
      state.messages=[...state.messages,message];
  }
};

export default {
  state,
  getters,
  actions,
  mutations,
};
