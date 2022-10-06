import { IBaseEntityMapper } from "~/app/_interfaces/mappers/ibase-entity-mapper.interface";
import { IBaseDtoEntityMapper } from "~/app/_interfaces/mappers/ibase-dto-entity-mapper.interface";
import { TermMiscellaneousFormSource } from "~/app/_models/form/term-miscellaneous-form.model";
import { TermMiscellaneous } from "~/app/_models/term-miscellaneous.model";
import { TermMiscellaneousDto } from "~/app/_dtos/term-miscellaneous.dto";

export class TermMiscellaneousMapper
  implements
    IBaseEntityMapper<TermMiscellaneous>,
    IBaseDtoEntityMapper<
      TermMiscellaneous,
      TermMiscellaneousFormSource,
      TermMiscellaneousDto
    >
{
  mapEntity(tm: TermMiscellaneous): TermMiscellaneous {
    const termMiscellaneous = new TermMiscellaneous();
    termMiscellaneous.termId = tm.termId;
    termMiscellaneous.id = tm.id;
    termMiscellaneous.name = tm.name;
    termMiscellaneous.description = tm.description;
    termMiscellaneous.amount = tm.amount;
    return termMiscellaneous;
  }

  initFormSource(): TermMiscellaneousFormSource {
    const termMiscellaneousFormSource = new TermMiscellaneousFormSource(
      "",
      "",
      0.0
    );

    return termMiscellaneousFormSource;
  }

  mapFormSourceToDto(tmf: TermMiscellaneousFormSource): TermMiscellaneousDto {
    const termMiscellaneousDto = new TermMiscellaneousDto(
      tmf.name,
      tmf.description,
      tmf.amount
    );
    return termMiscellaneousDto;
  }

  mapEntityToFormSource(
    termMiscellaneous: TermMiscellaneous
  ): TermMiscellaneousFormSource {
    const termMiscellaneousFormSource = new TermMiscellaneousFormSource(
      termMiscellaneous.name,
      termMiscellaneous.description,
      termMiscellaneous.amount
    );
    return termMiscellaneousFormSource;
  }

  mapFormSourceToEntity(
    formSource: TermMiscellaneousFormSource
  ): TermMiscellaneous {
    throw new Error("Not implemented");
  }
}
