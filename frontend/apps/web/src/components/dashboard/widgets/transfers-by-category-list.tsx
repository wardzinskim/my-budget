import {
  Card,
  CardHeader,
  Divider,
  LinearProgress,
  List,
  ListItem,
  ListItemText,
} from '@mui/material';
import { CategoryValue, TransferDTOType } from '@repo/api-client';
import { fCurrency } from '@repo/minimal-ui';
import React from 'react';

interface TransfersByCategoryListProps {
  categories: CategoryValue[];
  transferType?: TransferDTOType;
  title?: string;
  maxItems?: number;
}

type LinearProgressColor =
  | 'primary'
  | 'secondary'
  | 'success'
  | 'warning'
  | 'error';

const getBarColor = (
  value: number,
  transferType?: TransferDTOType
): LinearProgressColor => {
  if (transferType === TransferDTOType.Income) return 'success';
  if (transferType === TransferDTOType.Expense) return 'primary';
  return value >= 0 ? 'success' : 'error';
};

export const TransfersByCategoryList: React.FC<
  TransfersByCategoryListProps
> = ({ categories, transferType, title, maxItems }) => {
  const sorted = [...categories].sort(
    (a, b) => Math.abs(b.value ?? 0) - Math.abs(a.value ?? 0)
  );
  const visible = maxItems != null ? sorted.slice(0, maxItems) : sorted;
  const maxAbsValue = Math.abs(visible[0]?.value ?? 1) || 1;
  const hiddenCount = sorted.length - visible.length;

  return (
    <Card>
      <CardHeader
        title={!title ? <>Yearly {transferType}s grouped by category</> : title}
        subheader={
          hiddenCount > 0
            ? `Top ${maxItems} of ${sorted.length} categories`
            : undefined
        }
      />

      <List>
        {visible.map((category) => {
          const value = category.value ?? 0;
          const pct = Math.round((Math.abs(value) / maxAbsValue) * 100);
          const color = getBarColor(value, transferType);

          return (
            <React.Fragment key={category.category}>
              <ListItem secondaryAction={<span>{fCurrency(value)} PLN</span>}>
                <ListItemText
                  primary={category.category}
                  secondaryTypographyProps={{ component: 'div' } as object}
                  secondary={
                    <LinearProgress
                      variant="determinate"
                      value={pct}
                      color={color}
                    />
                  }
                />
              </ListItem>
              <Divider component="li" variant="middle" />
            </React.Fragment>
          );
        })}
      </List>
    </Card>
  );
};
