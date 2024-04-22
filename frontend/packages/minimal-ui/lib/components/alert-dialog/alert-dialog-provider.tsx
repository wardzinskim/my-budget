import { PropsWithChildren, useState } from 'react';
import { AlertDialog } from './alert-dialog';
import {
  AlertDialogContext,
  AlertDialogContextState,
} from './alert-dialog-context';

interface AlertDialogProvider extends PropsWithChildren {}

export const AlertDialogProvider: React.FC<AlertDialogProvider> = ({
  children,
}) => {
  const [alertDialogState, setAlertDialogState] =
    useState<AlertDialogContextState>({
      open: false,
      content: '',
      closeHandle: () => {},
    });

  const closeHandle: (result: boolean) => void = (result: boolean) => {
    alertDialogState.closeHandle(result);
    setAlertDialogState({
      ...alertDialogState,
      open: false,
    });

    setTimeout(() => {
      setAlertDialogState({
        open: false,
        title: undefined,
        content: '',
        closeHandle: () => {},
        buttonProps: undefined,
      });
    }, 200);
  };

  return (
    <AlertDialogContext.Provider
      value={{
        alertContext: alertDialogState,
        setAlertContext: setAlertDialogState,
      }}
    >
      {children}

      <AlertDialog
        open={alertDialogState.open}
        handleClose={closeHandle}
        content={alertDialogState.content}
        title={alertDialogState.title}
        buttonProps={alertDialogState.buttonProps}
      ></AlertDialog>
    </AlertDialogContext.Provider>
  );
};
