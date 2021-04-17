import axios from 'axios';
import jwt_decode from 'jwt-decode';

const state={
    user: initUser(),
};

const getters={
    getUsername: (state)=>state.user.username,
    isAuthenticated: ()=> localStorage.getItem('token')!==null 
};

const actions={
    async signIn(context,form){
        var response=await axios.post('https://localhost:44310/api/auth/login',form);
        var token=response.data.token;
        localStorage.setItem('token',token);
        context.commit('setUser',token);
    },
    async signUp(context,form){
        await axios.post('https://localhost:44310/api/auth/register',form);
    }
};

const mutations={
    setUser(state,token){
        state.token=token;
        let decoded = jwt_decode(token);
        console.log(decoded);
        state.user={
            id: decoded.sub,
            username: decoded.username
        };
    }
};

function initUser(){
    let token=localStorage.getItem('token');
    try{
        let decoded = jwt_decode(token);
        return {
            id: decoded.sub,
            username: decoded.username
        };

    }
    catch(e){
        return {}
    }
}

export default {
    state,
    getters,
    actions,
    mutations
};