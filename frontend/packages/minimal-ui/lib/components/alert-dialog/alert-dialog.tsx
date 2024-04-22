import {
  Button,
  ButtonOwnProps,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from '@mui/material';

export interface AlertDialogButtonProps {
  acceptButton?: ButtonOwnProps;
  acceptButtonLabel?: string;
  rejectButton?: ButtonOwnProps;
  rejectButtonLabel?: string;
}

interface AlertDialogProps {
  open: boolean;
  handleClose: (result: boolean) => void;
  title?: string;
  content: string;
  buttonProps?: AlertDialogButtonProps;
}

export const AlertDialog: React.FC<AlertDialogProps> = ({
  open,
  handleClose,
  title,
  content,
  buttonProps,
}) => {
  return (
    <Dialog
      open={open}
      onClose={() => handleClose(false)}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
    >
      {title && <DialogTitle id="alert-dialog-title">{title}</DialogTitle>}
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          {content}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button
          variant="contained"
          color="primary"
          {...buttonProps?.acceptButton}
          onClick={() => handleClose(true)}
        >
          {buttonProps?.acceptButtonLabel ?? 'OK'}
        </Button>
        <Button
          variant="outlined"
          color="inherit"
          onClick={() => handleClose(false)}
        >
          {buttonProps?.rejectButtonLabel ?? 'Cancel'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};
