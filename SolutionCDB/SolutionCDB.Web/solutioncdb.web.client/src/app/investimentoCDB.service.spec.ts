import { TestBed } from '@angular/core/testing';

import { InvestimentoCDBService } from './investimentoCDB/investimentoCDB.service';

describe('InvestimentoService', () => {
  let service: InvestimentoCDBService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvestimentoCDBService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
