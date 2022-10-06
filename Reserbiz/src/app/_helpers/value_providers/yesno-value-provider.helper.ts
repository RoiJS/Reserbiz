import { TranslateService } from "@ngx-translate/core";

import { YesNoEnum } from "~/app/_enum/yesno-unit.enum";
import { IYesNoValueProvider } from "~/app/_interfaces/value_providers/iyesno-value-provider.interface";

export class YesNoValueProvider implements IYesNoValueProvider {
  constructor(private translateService: TranslateService) {}

  get yesNoOptions(): Array<{ key: YesNoEnum; label: string }> {
    return [
      {
        key: YesNoEnum.Yes,
        label: this.translateService.instant("GENERAL_TEXTS.YES"),
      },
      {
        key: YesNoEnum.No,
        label: this.translateService.instant("GENERAL_TEXTS.NO"),
      },
    ];
  }
}
