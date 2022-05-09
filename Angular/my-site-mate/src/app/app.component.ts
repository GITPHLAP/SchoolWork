import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'my-site-mate';
  name = 'Kasper Jeppesen';
  shortDescription = 'Er en frisk ung gut, med masser af sjov. Han er elendig til bordtennis, men et geni til bordfodbold.';
  propertiesHeader = 'Kasper har desuden følgende egenskaber';
  properties: string[] = [
    'Han har kørekort',
    'Han kan for det meste låne fars bil',
    'Han er måske jordens største nærrig røv',
    'Sidst, men ikke mindst er han irriterende god til alt han rør ved',
    'med undtagelse for piger... ;)'
  ];
  
}
