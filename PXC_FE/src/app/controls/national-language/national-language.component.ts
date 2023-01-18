import {
  Component,
  Inject,
  LOCALE_ID,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { CldrIntlService, IntlService } from '@progress/kendo-angular-intl';
import { EnumLangID } from 'src/app/common/enum';
import { LocalStorage } from 'src/app/common/local-storage';

export interface Language {
  ID: string;
  Name: string;
  Img: string;
  IsHover: boolean;
}

@Component({
  selector: 'app-national-language',
  templateUrl: './national-language.component.html',
  styleUrls: ['./national-language.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class NationalLanguageComponent implements OnInit {
  languages: Array<Language> = [
    {
      ID: EnumLangID[EnumLangID.VIE],
      Name: 'Việt Nam',
      Img: 'VIE.png',
      IsHover: false,
    },
  
    /* {
      ID: EnumLangID[EnumLangID.ENG],
      Name: 'English',
      Img: 'ENG.png',
      IsHover: false,
    }, */
  ];

  clickOutLanguage: any;

  state = {
    showSelectLanguage: false,
  };

  /**
   * ngôn ngữ đang chọn
   */
  langSelected: any;

  constructor(
    private localStorage: LocalStorage,
    @Inject(LOCALE_ID) public localeId: string,
    public intlService: IntlService
  ) {}

  ngOnInit(): void {
    const fn = function (this: NationalLanguageComponent, $event: any) {
      if (!$event?.target?.closest('.language-selected')) {
        this.state.showSelectLanguage = false;
      }
    };
    this.clickOutLanguage = fn.bind(this);
    document.addEventListener('click', this.clickOutLanguage);
    this.langSelected = this.languages.find(
      (x) => x.ID == EnumLangID[this.localStorage.LangID]
    );
    this.changeLOCALE_ID(this.langSelected);
  }

  private changeLOCALE_ID(lang: Language){
    
    if(lang.ID == EnumLangID[EnumLangID.ENG]){
      this.localeId = 'en-US';
    (<CldrIntlService>this.intlService).localeId = 'en-US';
    }else{
      this.localeId = 'vi-VN';
    (<CldrIntlService>this.intlService).localeId = 'vi-VN';
    }
  }

  /**
   * Xử lý khi nhấn vào img ngôn ngữ, hiển thị tùy chọn ngôn ngữ
   * @param $event
   */
  public languageClick($event: Event) {
    this.state.showSelectLanguage = !this.state.showSelectLanguage;
  }

  /**
   *  itemLanguage_Selected
   */
  public itemLanguage_Selected(lang: Language, $event: Event) {
    if (lang && lang.ID != this.langSelected.ID) {
      let langId: EnumLangID = EnumLangID[lang.ID as keyof typeof EnumLangID];
      this.langSelected = lang;
      this.localStorage.LangID = langId;
      this.changeLOCALE_ID(lang);
    }
  }
}
