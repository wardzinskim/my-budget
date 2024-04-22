import { ButtonOwnProps } from '@mui/material';
import { Dispatch, SetStateAction, createContext, useContext } from 'react';
import { AlertDialogButtonProps } from './alert-dialog';

export interface AlertDialogContextState {
  open: boolean;
  title?: string;
  content: string;
  closeHandle: (result: boolean) => void;
  buttonProps?: {
    acceptButton?: ButtonOwnProps;
    acceptButtonLabel?: string;
    rejectButton?: ButtonOwnProps;
    rejectButtonLabel?: string;
  };
}

export interface IAlertDialogContext {
  alertContext: AlertDialogContextState;
  setAlertContext: Dispatch<SetStateAction<AlertDialogContextState>>;
}

export const AlertDialogContext = createContext<IAlertDialogContext>({
  alertContext: { open: false, content: '', closeHandle: () => {} },
  setAlertContext: () => {},
});

export interface AlertService {
  show: (
    title: string | null,
    content: string,
    closeHandle: (result: boolean) => void,
    buttonProps?: AlertDialogButtonProps
  ) => void;
}

export const useAlert: () => AlertService = () => {
  const alertContext = useContext(AlertDialogContext);

  const showAlert = (
    title: string | null,
    content: string,
    closeHandle: (result: boolean) => void,
    buttonProps?: AlertDialogButtonProps
  ) => {
    if (!alertContext.alertContext.open) {
      alertContext.setAlertContext({
        open: true,
        content: content,
        title: title ?? undefined,
        closeHandle: closeHandle,
        buttonProps: buttonProps,
      });
    }
  };

  return { show: showAlert };
};
