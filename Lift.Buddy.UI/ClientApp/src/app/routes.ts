import { NavigationMenu } from "./Model/NavigationMenu"

export const generalNavigationMenu: NavigationMenu[] = [
  //{ path: 'home', icon: 'home', name: 'home' },
  { path: 'search', icon: 'search', name: 'search' },
  { path: 'user', icon: 'user', name: 'user' },
  { name: 'Records', icon: 'dumbbell', path: 'pr' },
  { name: 'workouts', icon: 'calendar', path: 'workout/training' },
  { name: 'settings', icon: 'gear', path: 'settings' },
  { name: 'my athletes', icon: 'people-group', path: 'my-athletes' },
]

export const workoutNavigationMenu: NavigationMenu[] = [
  { name: 'todays workout', icon: 'calendar', path: 'workout/training' },
  { name: 'my workouts', icon: 'calendar', path: 'workout/my-workouts' },
  { name: 'create workout', icon: 'calendar-plus', path: 'workout/add/new' }
]

export const trainerNavigationMenu: NavigationMenu[] = [
  { name: 'home', icon: 'people-group', path: 'my-athletes' },
  { name: 'requests', icon: 'clipboard', path: 'requests' },
  { name: 'frontpage', icon: 'file', path: 'frontpage' },
]
