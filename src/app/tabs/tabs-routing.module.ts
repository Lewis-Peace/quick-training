import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: 'tabs',
    component: TabsPage,
    children: [
      {
        path: 'massimali',
        loadChildren: () => import('../Pagine/massimali/massimali.module').then(m => m.MassimaliPageModule)
      },
      { path: 'allenamento', loadChildren: () => import('../Pagine/allenamento/allenamento.module').then(m => m.AllenamentoPageModule) },
      { path: 'allenamento/:folder', loadChildren: () => import('../Pagine/allenamento/allenamento.module').then(m => m.AllenamentoPageModule) },
      {
        path: 'cronometro',
        loadChildren: () => import('../Pagine/cronometro/cronometro.module').then(m => m.CronometroPageModule)
      },
      {
        path: '',
        redirectTo: '/tabs/allenamento',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    redirectTo: '/tabs/allenamento',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class TabsPageRoutingModule {}
