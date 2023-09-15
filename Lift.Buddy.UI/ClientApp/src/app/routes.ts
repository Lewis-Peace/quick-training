import { NavigationMenu } from "./Model/NavigationMenu"

export const generalNavigationMenu: NavigationMenu[] = [
  { path: 'home', icon: 'home', name: 'home' },
  { path: '', icon: 'search', name: 'search' },
  {path: 'user', icon: 'user', name: 'user',  },
  { name: 'Records', icon: 'dumbbell', path: 'pr' },
  {name: 'workouts', icon: 'calendar', path: 'workout/training'},
]

export const workoutNavigationMenu: NavigationMenu[] = [
  {name: 'home', icon: 'calendar', path: 'workout/training'},
  {name: 'my workouts', icon: 'calendar', path: 'workout/my-workouts'},
  {name: 'create workout', icon: 'calendar-plus', path: 'workout/add/-1'}
]
