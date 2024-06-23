import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ColigacaoService } from '../../services/coligacao/coligacao.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Coligacao } from '../../shared/models/interfaces/coligacao';
import { FormValidator } from '../../util/class';

@Component({
  selector: 'app-coligacoes',
  templateUrl: './coligacoes.component.html',
})
export class ColigacoesComponent implements OnInit {

  ngOnInit(): void {
    
  }

}
