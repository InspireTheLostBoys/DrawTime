import Vue from 'vue'
import VueRouter from 'vue-router'

import Main from '../views/Index.vue'

// Landing
import Landing from '../views/Landing/Index.vue'

// Draws
import DrawsCreate from '../views/Draws/Create.vue'
import DrawsEdit from '../views/Draws/Edit.vue'

// Participants
import Participants from '../views/Participants/Index.vue'

// Settings
import Settings from '../views/Settings/Index.vue'
import SystemUsers from '../views/Settings/SystemUsers/Index.vue'
import CreateSystemUsers from '../views/Settings/SystemUsers/Create.vue'
import EditSystemUsers from '../views/Settings/SystemUsers/Edit.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'main',
    component: Main,
    children: [
      {
        path: '/Landing',
        name: 'landing',
        component: Landing,
      },
      {
        path: '/CreateDraw',
        name: 'draws-create',
        component: DrawsCreate,
      },
      {
        path: '/EditDraw/:draw_id',
        name: 'draws-edit',
        component: DrawsEdit,
      },
      {
        path: '/Participants/:draw_id',
        name: 'participants',
        component: Participants,
      },
      {
        path: '/Settings',
        name: 'settings',
        component: Settings,
      },
      {
        path: '/SystemUsers',
        name: 'system-users',
        component: SystemUsers,
      },
      {
        path: '/SystemUsers/Create',
        name: 'system-users-create',
        component: CreateSystemUsers,
      },
      {
        path: '/SystemUsers/Edit/:user_id',
        name: 'system-users-edit',
        component: EditSystemUsers,
      }
    ]
  }
]

const router = new VueRouter({
  routes
})

export default router
