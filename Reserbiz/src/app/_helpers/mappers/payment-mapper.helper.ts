import { PaymentForTypeEnum } from '../../_enum/payment-type.enum';
import { PaymentDto } from '../../_dtos/payment-dto';

import { IBaseDtoEntityMapper } from '../../_interfaces/mappers/ibase-dto-entity-mapper.interface';
import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';

import { PaymentFormSource } from '../../_models/form/payment-form.model';
import { Payment } from '../../_models/payment.model';

export class PaymentMapper
  implements
    IBaseEntityMapper<Payment>,
    IBaseDtoEntityMapper<Payment, PaymentFormSource, PaymentDto>
{
  mapEntity(p: Payment): Payment {
    const payment = new Payment();

    payment.accountStatementId = p.accountStatementId;
    payment.dateTimeReceived = new Date(p.dateTimeReceived);
    payment.amount = p.amount;
    payment.receivedBy = p.receivedBy;
    payment.notes = p.notes;
    payment.isAmountFromDeposit = p.isAmountFromDeposit;
    payment.paymentForType = p.paymentForType;
    return payment;
  }

  initFormSource(): PaymentFormSource {
    const paymentFormSource = new PaymentFormSource(
      new Date(),
      new Date(),
      0,
      '',
      false,
      PaymentForTypeEnum.Rental,
      ''
    );
    return paymentFormSource;
  }

  mapFormSourceToDto(paymentFormSource: PaymentFormSource): PaymentDto {
    // Set time recieved (HH:mm)
    paymentFormSource.dateReceived.setHours(
      paymentFormSource.timeReceived.getHours(),
      paymentFormSource.timeReceived.getMinutes()
    );

    const paymentDto = new PaymentDto(
      paymentFormSource.dateReceived,
      paymentFormSource.amount,
      paymentFormSource.notes,
      paymentFormSource.isAmountFromDeposit,
      paymentFormSource.paymentForType
    );

    return paymentDto;
  }

  mapEntityToFormSource(payment: Payment): PaymentFormSource {
    const paymentFormSource = new PaymentFormSource(
      payment.dateTimeReceived,
      payment.timeReceived,
      payment.amount,
      payment.notes,
      payment.isAmountFromDeposit,
      payment.paymentForType,
      payment.receivedBy
    );
    return paymentFormSource;
  }

  mapFormSourceToEntity(paymentFormSource: PaymentFormSource): Payment {
    const payment = new Payment();

    // Set time recieved (HH:mm)
    paymentFormSource.dateReceived.setHours(
      paymentFormSource.timeReceived.getHours(),
      paymentFormSource.timeReceived.getMinutes()
    );

    payment.dateTimeReceived = paymentFormSource.dateReceived;
    payment.amount = paymentFormSource.amount;
    payment.receivedBy = paymentFormSource.receivedBy;
    payment.notes = paymentFormSource.notes;
    payment.isAmountFromDeposit = paymentFormSource.isAmountFromDeposit;
    payment.paymentForType = paymentFormSource.paymentForType;

    return payment;
  }
}
